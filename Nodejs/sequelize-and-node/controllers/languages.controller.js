module.exports = (db) => {

    const uuidv4 = require('uuid').v4();
    const Language = db.Language;

    class LanguageController {

        async findAll(req, res) {
            try {
                const languages = await Language.findAll({ include: ["user"] });
                // console.log("languages", languages);
                return res.json(languages);
            } catch (err) {
                console.log('There was an error querying languages', err);
                return res.status(500).json(err);
            }
        }

        async findById(req, res) {
            try {
                /* const [language] = await Language.findAll({
                     limit: 1,
                     where: {
                         id: req.params.id
                         //your where conditions, or without them if you need ANY entry
                     },
                     order: [ [ 'createdAt', 'DESC' ]]
                 }); */

                const language = await Language.findOne({ where: { id: req.params.id }, include: ["user"] });
                return res.json(language);
            } catch (err) {
                console.log('***There was an error getting a language', err);
                return res.status(400).json(err);
            }
        }

        async create(req, res) {
            try {
                const language = new  Language({
                    title: req.body.title,
                    speak: req.body.speak,
                    read: req.body.read,
                    userId: req.body.userId,
                    understand: req.body.understand,
                    slug: uuidv4.replace('-','')
                });
                await language.save();
                return res.json(language);
            } catch (err) {
                console.log('***There was an error creating a language',err);
                return res.status(500).json(err);
            }
        }

        async put(req, res) {
            try {
                // console.log('db', Language);
                const language = await Language.findByPk(parseInt(req.params.id));
                const newlanguage = {
                    title: req.body.title,
                    speak: req.body.speak,
                    read: req.body.read,
                    userId: req.body.userId,
                    understand: req.body.understand,
                    slug: req.body.slug,
                };

                await language.update(newlanguage);
                return res.json(language);
            } catch (err) {
                console.log('***There was an error updating a language', err);
                return res.status(500).json(err);
            }
        }

        async delete(req, res) {
            try {
                const language = await Language.findByPk(req.body.id);

                if (language) {
                    await language.destroy({force: true});
                }
                return res.json({'message': 'deleted'});
            } catch (err) {
                console.log('***Error deleting language',err);
                return res.status(400).json(err);
            }
        }
    }

    return  new LanguageController();
};
