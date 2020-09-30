'use strict';

module.exports = (routes, db, authenticateToken) => {
    const LanguageController = require('../../controllers/languages.controller')(db);
    routes.get('/languages', authenticateToken, LanguageController.findAll);
    routes.get('/languages/:id', authenticateToken, LanguageController.findById);
    routes.post('/languages', authenticateToken, LanguageController.create);
    routes.delete('/languages/:id', authenticateToken, LanguageController.delete);
    routes.put('/languages/:id', authenticateToken, LanguageController.put);
};
