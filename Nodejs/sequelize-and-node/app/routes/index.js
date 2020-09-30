'use strict';

module.exports = (routes, db, authGrade) => {
    // language all routes
    require('./languages.route')(routes, db, authGrade.authenticateToken);
    require('./users.route')(routes, db, authGrade);
    // rest
};
