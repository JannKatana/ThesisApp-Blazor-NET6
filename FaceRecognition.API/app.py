
import json
from xmlrpc.client import boolean
from flask import Flask, jsonify, request, redirect, url_for
from flask_cors import CORS

from facenet_pytorch import MTCNN, InceptionResnetV1
import torch
from torch.utils.data import DataLoader
from torchvision import datasets
import numpy as np
import pandas as pd
import os

workers = 0 if os.name == 'nt' else 4

device = torch.device('cuda:0' if torch.cuda.is_available() else 'cpu')
print('Running on device: {}'.format(device))

mtcnn = MTCNN(
    image_size=160, margin=0, min_face_size=20,
    thresholds=[0.6, 0.7, 0.7], factor=0.709, post_process=True,
    device=device,
    keep_all=True)

resnet = InceptionResnetV1(pretrained='vggface2').eval().to(device)


def collate_fn(x):
    return x[0]

dataset = datasets.ImageFolder('static/uploads/known_faces/')
dataset.idx_to_class = {i: c for c, i in dataset.class_to_idx.items()}
loader = DataLoader(dataset, collate_fn=collate_fn, num_workers=workers)

known_aligned = []
known_names = []

for x, y in loader:
    x_aligned, _ = mtcnn(x, return_prob=True)
    if x_aligned is not None:
        for index, face in enumerate(x_aligned):
            known_aligned.append(face)
            known_names.append(dataset.idx_to_class[y] + str(index))


app = Flask(__name__)
cors = CORS(app, resources={r"/*": {"origins": "*"}})
app.config['CORS_HEADERS'] = 'Content-Type'


@app.route('/face-recognition/', methods=['POST'])
def perform_face_recognition():
    try:
        req_image = request.files.get('image')
        filename = 'unknown.jpg'
        req_image.save('./static/uploads/unknown/' + filename)

        unknown = datasets.ImageFolder('./static/uploads/unknown')
        unknown.idx_to_class = {i: c for c, i in unknown.class_to_idx.items()}
        unknown_loader = DataLoader(
            unknown, collate_fn=collate_fn, num_workers=workers)

        aligned = [*known_aligned]
        names = [*known_names]
        unknown_faces = []
        for x, y in unknown_loader:
            x_aligned, _ = mtcnn(x, return_prob=True)
            if x_aligned is not None:
                for index, face in enumerate(x_aligned):
                    aligned.append(face)
                    names.append(unknown.idx_to_class[y] + str(index))
                    unknown_faces.append(unknown.idx_to_class[y] + str(index))

        aligned = torch.stack(aligned).to(device)
        embeddings = resnet(aligned).detach().cpu()

        dists = [[(e1 - e2).norm().item() for e2 in embeddings]
                 for e1 in embeddings]
        df = pd.DataFrame(dists, columns=names, index=names)
        df = df.drop(columns=known_names)
        df = df.drop(index=unknown_faces)

        return df.to_json()

    except Exception as ex:
        return jsonify({'message': str(ex)}), 500


@app.route('/register/', methods=['POST'])
def register_new_face():
    try:
        data = json.loads(request.form.get('data'))
        file = request.files['file']
        face_id = data['face-id']

        filename = "face.jpg"
        file.save('./static/uploads/known_faces/' + face_id + '/' + filename)

    except Exception as ex:
        return jsonify({'message': str(ex)})

    return jsonify({'message': 'face image stored successfully'})


@app.route('/display-image/<filename>')
def display_image(filename):
    # http://127.0.0.1:5000/display-image/6071b602-9020-48c5-b0b0-9b088c412c88
    return redirect(url_for('static', filename='uploads/known_faces/' + filename + '/face.jpg'), code=301)


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True, threaded=True)
