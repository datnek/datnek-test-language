'use strict';
// process.env.NODE_ENV = 'development';
const app = require('./app/index');

if (process.env.NODE_ENV !== 'test') {
    app.listen(process.env.NODE_PORT || 3000, () => {
        console.log(`Server is up on port ${process.env.NODE_PORT || 3000}`);
    });
}

module.exports = app;
