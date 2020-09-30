'use strict';


const express = require('express');
var cors = require('cors');
const db = require('../models');
const authGrade = require('./auth.grade');
const routes = new express.Router();
require('./routes/index')(routes, db, authGrade());



class App {
    constructor() {
        this.express = express();
        this.database();
        this.middlewares();
        this.routes();
    }

    database() {
        // TODO
    }

    middlewares() {
        // this.express.use(bodyParser.json());
        this.express.use(express.static(__dirname + '/public'));
        this.express.use(express.json()); // same this.express.use(bodyParser.json())
        this.express.use(cors());
    }

    routes() {
        this.express.use('/api', routes);
    }
}

module.exports = new App().express;
