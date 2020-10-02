'use strict';

module.exports = (db,authGrade) => {
    const md5 = require('md5');
    const uuidv4 = require('uuid').v4();
    const User = db.User;

    class UserController {

        async findAll(req, res) {
            try {
                const users = await User.findAll({ include: ["languages"] });
                // console.log("users", users);
                return res.json(users);
            } catch (err) {
                console.log('There was an error querying users', err);
                return res.status(500).json(err);
            }
        }

        async findById(req, res) {
            try {
                /* const [user] = await User.findAll({
                     limit: 1,
                     where: {
                         id: req.params.id
                         //your where conditions, or without them if you need ANY entry
                     },
                     order: [ [ 'createdAt', 'DESC' ]]
                 }); */

                const user = await User.findOne({ where: { id: req.params.id }, include: ["languages"] });
                return res.json(user);
            } catch (err) {
                console.log('***There was an error getting a user', err);
                return res.status(400).json(err);
            }
        }

        async create(req, res) {
            try {
                const user = new  User({
                    slug: uuidv4.replace('-',''),
                    username: req.body.username,
                    email: req.body.email,
                    password: md5(req.body.password)
                });
                await user.save();
                return res.json(user);
            } catch (err) {
                console.log('***There was an error creating a user',err);
                return res.status(500).json(err);
            }
        }

        async put(req, res) {
            try {
                // console.log('db', User);
                const user = await User.findByPk(parseInt(req.params.id));
                const newuser = {
                    slug: req.body.slug,
                    username: req.body.username,
                    email: req.body.email,
                    password: req.body.password
                };

                await user.update(newuser);
                return res.json(user);
            } catch (err) {
                console.log('***There was an error updating a user', err);
                return res.status(500).json(err);
            }
        }

        async delete(req, res) {
            try {
                const user = await User.findByPk(req.body.id);

                if (user) {
                    await user.destroy({force: true});
                }
                return res.json({'message': 'deleted'});
            } catch (err) {
                console.log('***Error deleting user',err);
                return res.status(400).json(err);
            }
        }

        generateToken(req, res){
            const token = authGrade.generateAccessToken({ username: req.body.username });
            res.json(token);
        }
    }

    return  new UserController();
};
