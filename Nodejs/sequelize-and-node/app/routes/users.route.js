'use strict';

module.exports = (routes, db, authgrade) => {
    const UserController = require('../../controllers/users.controller')(db,authgrade);

    routes.get('/users/token', UserController.generateToken);
    routes.get('/users',authgrade.authenticateToken, UserController.findAll);
    routes.get('/users/:id',authgrade.authenticateToken, UserController.findById);
    routes.post('/users', authgrade.authenticateToken, UserController.create);
    routes.delete('/users/:id', authgrade.authenticateToken, UserController.delete);
    routes.put('/users/:id', authgrade.authenticateToken, UserController.put);
};
