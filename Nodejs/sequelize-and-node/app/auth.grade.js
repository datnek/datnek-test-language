const jwt = require("jsonwebtoken");
// get config vars
const dotenv = require("dotenv").config();

module.exports = () => {
    class AuthGrade {
        authenticateToken(req, res, next) {
            // Gather the jwt access token from the request header
            const authHeader = req.headers['authorization'];
            const token = authHeader && authHeader.split(' ')[1];
            if (token == null) return res.sendStatus(401); // if there isn't any token
            // console.log('debug', process.env.ACCESS_TOKEN_SECRET);

            jwt.verify(token, process.env.ACCESS_TOKEN_SECRET , (err, user) => {
                console.log(err);
                if (err) return res.sendStatus(403);
                req.user = user;
                next() // pass the execution off to whatever request the client intended
            });
        }

        // username is in the form { username: "my cool username" }
        // ^^the above object structure is completely arbitrary
        generateAccessToken(username) {
            // expires after half and hour (1800 seconds = 30 minutes)
            return jwt.sign(username, process.env.ACCESS_TOKEN_SECRET, { expiresIn: process.env.ACCESS_TOKEN_LIFE });
        }
    }

    return new AuthGrade();
};
