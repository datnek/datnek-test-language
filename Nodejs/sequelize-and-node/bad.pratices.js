'use strict';
const express = require('express');
const bodyParser = require('body-parser');
const db = require('./models'); // new require for db object

const app = express();

app.use(bodyParser.json());
app.use(express.static(__dirname + '/public'));

app.get('/api/languages',  (req, res) => {

    return db.language.findAll()
        .then((languages) => res.send(languages))
        .catch((err) => {
            console.log('There was an error querying languages', JSON.stringify(err));
            return res.send(err)
        });
});

app.post('/api/languages', (req, res) => {
    const { title, speak, understand, read } = req.body;
    return db.language.create({ title, speak, understand, read })
        .then((language) => res.send(language))
        .catch((err) => {
            console.log('***There was an error creating a language', JSON.stringify(language));
            return res.status(400).send(err)
        })
});

app.delete('/api/languages/:id', (req, res) => {
    const id = parseInt(req.params.id);
    return db.language.findById(id)
        .then((language) => language.destroy({ force: true }))
        .then(() => res.send({ id }))
        .catch((err) => {
            console.log('***Error deleting language', JSON.stringify(err));
            res.status(400).send(err)
        })
});

app.put('/api/languages/:id', (req, res) => {
    const id = parseInt(req.params.id);
    return db.language.findById(id)
        .then((language) => {
            const { title, speak, understand, read } = req.body;
            return language.update({ title, speak, understand, read })
                .then(() => res.send(language))
                .catch((err) => {
                    console.log('***Error updating language', JSON.stringify(err));
                    res.status(400).send(err)
                })
        })
});

app.listen(3000, () => {
    console.log('Server is up on port 3000');
});
