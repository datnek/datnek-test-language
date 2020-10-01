# I. Sujet
Vous souhaitez aider Datnek à informatiser un système de gestion des langues internationaux afin de repérer le niveau de langue de ses employés.

L’application devra être développé en backend avec nodejs et frontend angular. Le candidat doit faire le minimum de test unitaire et bien commenter son code.
Pour une langue choisie, il faudra préciser le niveau parlé, écrit et compréhension.
Pour le projet frontend, le candidat devra utiliser les notions suivantes : input, output, sujet/suscription, injection de dépendance.
Pour le backend, le candidat devra utiliser nodejs MVC et une base de données MySQL. Il est libre de faire les tests avec MongoDB, SQLite ou tout autre base de données en mémoire.

Quand l'utilisateur est connecter, il pourra ajouter, modifier, supprimer ou consulter le détail de la liste de ces langues.

Il n'est pas necessaire de créer 3 tables. deux suffironts pour la gestion. 
- Une table user: Pour la gestion des utilisateurs
- Une table langage: Pour la gestions des langues

Quand il clique sur détail, un modal doit souffrir et le statut du bouton close sur la barre de menu doit changer.

Quand il clique sur une langue, la page est traduite. Seul 3 langues sont obligatoires : français, anglais, néerlandais.

Le nombre de langues incrémente à chaque fois qu’il y’a une nouvelle entrée.
L’application doit être responsive.
 

# II.Initialisation du projet
- Installer sequelize en global
```bash
npm i sequelize-auto -g
```
- Créer le projet et se posiser dans le repertoire crée
```bash
mkdir datnek-test & cd datnek-test
```
- Initialiser npm
vous devez suivre kes étapes 
```bash
npm init 
```
- Installer les dépendances nécessaires

```bash
npm install --save express body-parser sequelize jsonwebtoken dotenv uuid md5 supertest sequelize-cli cross-env cors morgan mysql2 nodemon jest coverage node-coverage typescript ts-node @types/express @types/node
```
- [x] **express**: pour le routage 
- [x] **body-parser**: faciliter le mappage des données   
- [x] **sequelize**: ORM permettre de faire les migrations   
- [x] **jsonwebtoken**: pour la sécurité de l'api  
- [x] **dotenv**: gestion des variables environementales (.env)    
- [x] **uuid**: Pour la génération des indentifiants uniques   
- [x] **md5**: Pour le hashage du mot de passe 
- [x] **supertest et jest**: Pour le test unitaire
- [x] **sequelize-cli**: Pour faciliter exécuter les commandes sequelize
- [x] **cors**: Pourdonner la possibilité d'accèder au projet depuis un client
- [x] **cross-env**: Pour permettre de changer les paramètres environnementals depuis une ligne de commande par exemple change le mode developpement en test
- [x] **morgan**: Pour la journalisation
- [x] **sequelize-cli**: Pour le lancement de l'application et sa gestion
- [x] **overage et node-coverage**: Pour la couverture des tests unitaires

- Initailiser les migrations
```bash
sequelize init
```

# III. Modéle migrations et seed

- Générer l'entité Language
```bash
sequelize model:generate --name Language --attributes title:string,slug:string,speak:float,understand:float,read:float
```

- Générer l'entité User
```bash
sequelize model:generate --name User--attributes username:string,slug:string,email:string,password:string
```

- Modifier le contenu du modèle user à ceci
```js
'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class User extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
      // Un utilisateur pratique une ou plusieurs langues
      models.User.hasMany(models.Language, { as: "languages", onDelete: 'CASCADE' });
    }
  };
  User.init({
    slug: DataTypes.STRING,
    username: DataTypes.STRING,
    password: DataTypes.STRING,
    email: DataTypes.STRING
  }, {
    sequelize,
    modelName: 'User',
  });
  return User;
};

```
 

- Modifier le contenu du modèle language à ceci
```js
'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Language extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
      // Une langue est pratiquée par un utilisateur
      models.Language.belongsTo(models.User, {
        foreignKey: "userId",
        as: "user",
      });
    }
  };
  Language.init({
    title: DataTypes.STRING,
    speak: DataTypes.FLOAT,
    understand: DataTypes.FLOAT,
    read: DataTypes.FLOAT,
    slug: DataTypes.STRING,
    userId: DataTypes.INTEGER,
  }, {
    sequelize,
    modelName: 'Language',
  });
  return Language;
};

```
 

- Modifier la migration user à ceci
```js
'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.createTable('Users', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      slug: {
        type: Sequelize.STRING
      },
      username: {
        type: Sequelize.STRING
      },
      password: {
        type: Sequelize.STRING
      },
      email: {
        type: Sequelize.STRING
      },
      createdAt: {
        allowNull: false,
        type: Sequelize.DATE,
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP()')
      },
      updatedAt: {
        allowNull: false,
        type: Sequelize.DATE,
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP() ON UPDATE CURRENT_TIMESTAMP()')
      }
    });
  },
  down: async (queryInterface, Sequelize) => {
    await queryInterface.dropTable('Users');
  }
};

```

- Modifier la migration language à ceci
```js
'use strict';
module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.createTable('Languages', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      slug:{
        allowNull: false,
        type: Sequelize.STRING
      },
      title: {
        type: Sequelize.STRING
      },
      speak: {
        type: Sequelize.FLOAT
      },
      understand: {
        type: Sequelize.FLOAT
      },
      read: {
        type: Sequelize.FLOAT
      },
      userId:{
        allowNull: false,
        type: Sequelize.INTEGER
      },
      createdAt: {
        allowNull: false,
        type: Sequelize.DATE,
        // defaultValue: Sequelize.NOW
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP()')
      },
      updatedAt: {
        allowNull: false,
        type: Sequelize.DATE,
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP() ON UPDATE CURRENT_TIMESTAMP()')
      }
    });
  },
  down: async (queryInterface, Sequelize) => {
    await queryInterface.dropTable('Languages');
  }
};
```

# IV. Fichiers de configuration

- Ajouter ces informations dans package.json
```js
"main": "server.js",
  "scripts": {
    "test": "cross-env NODE_ENV=test jest --runInBand --detectOpenHandles --silent --forceExit --coverage ",
    "test:watch": "cross-env NODE_ENV=test jest --coverage --runInBand --watch",
    "start": "cross-env NODE_ENV=development  nodemon server.js",
    "dev": "nodemon ./server.js localhost 3010"
  },
  "jest": {
    "testEnvironment": "node"
  },
```


- Créer le fichier .env à la racine du projet
```js
touch .env
```

- Remplacer le contenu de .env par ceci
N'oublier pas de changer le token une fois que vous aurez génerez
```js
ACCESS_TOKEN_SECRET=datnekrefresh2020@contact
ACCESS_TOKEN_LIFE=1314000s
ACCESS_TOKEN=Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InN0cmF0Z2UiLCJpYXQiOjE2MDE0MzM0ODcsImV4cCI6MTYwMjc0NzQ4N30.hVrxEz-y2GbUuB0cvYofllRXVBUyKvWDZZA2QLKP86E
```



- Modifier les paramètres de connexion à la base de données dans config/config.json


- Créer la base de données
```js
sequelize db:migrate
```


- Mettre à jour la base de données
```js
sequelize db:migrate
```


- Créer le seed pour les utilisateurs
```js
sequelize seed:generate --name seed-user
```

- Dans le repertoire seeds, rempalcer le contenu de seed-user.js par
```js
'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    /**
     * Add seed commands here.
     *
     * Example:
     * await queryInterface.bulkInsert('People', [{
     *   name: 'John Doe',
     *   isBetaMember: false
     * }], {});
    */
    await queryInterface.bulkInsert('Users', [{
      email: 'danick.takam@datnek.be',
      username: "stratege",
      password: "password",
      slug: "123",
      id:1
    }, {
      email: 'arno.yankam@datnek.be',
      username: "arno",
      password: "password",
      slug: "524",
      id:2
    }], {});
  },

  down: async (queryInterface, Sequelize) => {
    /**
     * Add commands to revert seed here.
     *
     * Example:
     * await queryInterface.bulkDelete('People', null, {});
     */
    await queryInterface.bulkDelete('Users', null, {});
  }
};

```


- Créer le seed pour les languages
```js
sequelize seed:generate --name seed-language
```

- Dans le repertoire seeds, rempalcer le contenu de seed-language.js par
```js
'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    /**
     * Add seed commands here.
     *
     * Example:
     * await queryInterface.bulkInsert('People', [{
     *   name: 'John Doe',
     *   isBetaMember: false
     * }], {});
     */
    await queryInterface.bulkInsert('Languages', [{
      title: 'en',
      speak: 3,
      understand: 2,
      read: 4,
      userId: 1,
      slug:'785'
    }, {
      title: 'nl',
      speak: 1,
      understand: 2,
      read: 2,
      userId: 1,
      slug:'754'
    }], {});
  },

  down: async (queryInterface, Sequelize) => {
    /**
     * Add commands to revert seed here.
     *
     * Example:
     * await queryInterface.bulkDelete('People', null, {});
     */
    await queryInterface.bulkDelete('Languages', null, {});
  }
};

```

- Migrer les tous les seeds
```js
sequelize db:seed:all
```


# V. Tests unitaires

- Créer le repertoire tests à la racine du projet
```js
mkdir tests
```


- Créer le fichier pour mocker la base de données de test
```js
touch tests/db.handle.js
```

- Changer le contenu du fichier db.handle.js
```js
const util = require('util');
const exec = util.promisify(require('child_process').exec);
const dotenv = require("dotenv").config();

module.exports = () => {

    class DbHandle{

        constructor(){
            this.header = process.env.ACCESS_TOKEN;
        }
        async create(){
            await this.run("sequelize db:create");
        }

        async drop(){
            await this.run("sequelize db:drop");
        }

        async migrate(){
            await this.run("sequelize db:migrate");
        }

        async run(command) {
           try {
               const { stdout, stderr } = await exec(command);

               if (stderr) {
                   console.error(`error: ${stderr}`);
               }
               console.log(`stdout: ${stdout}`);
           } catch (e) {
               
           }
        }
    }
    return new DbHandle();
};

```

- Créer le dossier controllers
```js
mkdir controllers
```

- Créer le fichier de test users.controller.spec.js
```js
touch controllers/users.controller.spec.js
```

- Remplacer le contenu du fichier users.controller.spec.js
```js
'use strict';

const supertest = require('supertest');
const app = require('../server');
const dbHandle = require('../tests/db.handle')();
const request = supertest(app);
const url = "/api/users";

beforeEach(async () =>
{
    await dbHandle.create();
    await dbHandle.migrate();
});


afterEach(async () => {
    await dbHandle.create();
    await dbHandle.drop();
});


// afterAll(async ()  => await dbHandle.drop());

describe('UsersController',  () => {
    
    it('can be created correctly', async () => {

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(userComplete);

        //console.log('response', response);
        //console.log('response', response);
        expect(response.body.username).toBe(userComplete.username);
        expect(response.body.email).toBe(userComplete.email);
        expect(response.body.slug).toBeDefined();

    });

    it('can be edit correctly', async () => {

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(userComplete);

        expect(response.status).toBe(200);
        expect(response.body.username).toBe(userComplete.username);
        expect(response.body.email).toBe(userComplete.email);
        expect(response.body.slug).toBeDefined();

        const newUser = response.body;

        newUser.username = 'danick';
        newUser.email = 'otis.takam@datnek.be';


        const response2 = await request.put(`${url}/${newUser.id}`)
            .set('Authorization', dbHandle.header)
            .send(newUser);

        expect(response2.status).toBe(200);
        expect(response2.body.username).toBe(newUser.username);
        expect(response2.body.email).toBe(newUser.email);
        expect(response2.body.slug).toBeDefined();
    });


    it('can be get all correctly', async () => {

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(userComplete);

        expect(response.status).toBe(200);
        expect(response.body.username).toBe(userComplete.username);
        expect(response.body.email).toBe(userComplete.email);
        expect(response.body.slug).toBeDefined();

        //const newUser = response.body;

        const response2 = await request.get(url)
            .set('Authorization', dbHandle.header)
            .send();

        expect(response2.status).toBe(200);
        expect(response2.body[0].username).toBe(userComplete.username);

    });

    it('can be find by id correctly', async () => {

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(userComplete);

        expect(response.status).toBe(200);
        expect(response.body.username).toBe(userComplete.username);
        expect(response.body.email).toBe(userComplete.email);
        expect(response.body.slug).toBeDefined();

        const newUser = response.body;

        const response2 = await request.get(`${url}/${newUser.id}`)
            .set('Authorization', dbHandle.header)
            .send();

        expect(response2.status).toBe(200);
        expect(response2.body.username).toBe(userComplete.username);

    });

    it('can be delete correctly', async () => {

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(userComplete);

        expect(response.status).toBe(200);
        expect(response.body.username).toBe(userComplete.username);

        const newUser = response.body;

        const response2 = await request.delete(`${url}/${newUser.id}`)
            .set('Authorization', dbHandle.header)
            .send();
        expect(response2.status).toBe(200);
        expect(response2.body.message).toBe('deleted');

    });
});


const userComplete = {
    email: 'danick.takam@datnek.be',
    username: "stratege",
    password: "password",
    slug: '113'
};

```



- Créer le fichier de test languages.controller.spec.js
```js
touch controllers/languages.controller.spec.js
```

- Changer le contenu par 
```js
'use strict';

const supertest = require('supertest');
const app = require('../server');
const dbHandle = require('../tests/db.handle')();
const request = supertest(app);
const url = "/api/languages";
const userUrl = "/api/users";
let user = null;

beforeEach(async () =>
{
    await dbHandle.create();
    await dbHandle.migrate();

    //init user
     user = (await request.post(userUrl)
        .set('Authorization', dbHandle.header)
        .send(userComplete)).body;

});

afterEach(async () => {
    await dbHandle.create();
    await dbHandle.drop();
});

// afterAll(async ()  => await dbHandle.drop());

describe('LanguagesController',  () => {
    
    it('can be created correctly', async () => {

        languageComplete.userId = user.id;
        expect(user.username).toBe(userComplete.username);

        const response = await request.post(url)
                                .set('Authorization', dbHandle.header)
                                .send(languageComplete);

        //console.log('response', response);
        //console.log('response', response);
        expect(response.body.title).toBe(languageComplete.title);
        expect(response.body.speak).toBe(languageComplete.speak);
        expect(response.body.read).toBe(languageComplete.read);
        expect(response.body.understand).toBe(languageComplete.understand);

    });

    it('can be edit correctly', async () => {

        languageComplete.userId = user.id;
        expect(user.username).toBe(userComplete.username);

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(languageComplete);

        expect(response.status).toBe(200);
        expect(response.body.title).toBe(languageComplete.title);
        expect(response.body.speak).toBe(languageComplete.speak);
        expect(response.body.read).toBe(languageComplete.read);
        expect(response.body.understand).toBe(languageComplete.understand);

        const newLanguage = response.body;

        newLanguage.title = 'fr';
        newLanguage.speak = 4;
        newLanguage.understand = 4;
        newLanguage.read = 4;

        const response2 = await request.put(`${url}/${newLanguage.id}`)
            .set('Authorization', dbHandle.header)
            .send(newLanguage);

        expect(response2.status).toBe(200);
        expect(response2.body.title).toBe(newLanguage.title);
        expect(response2.body.speak).toBe(newLanguage.speak);
        expect(response2.body.read).toBe(newLanguage.read);
        expect(response2.body.understand).toBe(newLanguage.understand);
    });


    it('can be get all correctly', async () => {

        languageComplete.userId = user.id;
        expect(user.username).toBe(userComplete.username);

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(languageComplete);

        expect(response.status).toBe(200);
        expect(response.body.title).toBe(languageComplete.title);
        expect(response.body.speak).toBe(languageComplete.speak);
        expect(response.body.read).toBe(languageComplete.read);
        expect(response.body.understand).toBe(languageComplete.understand);

        //const newLanguage = response.body;

        const response2 = await request
            .get(url)
            .set('Authorization', dbHandle.header)
            .send();

        expect(response2.status).toBe(200);
        expect(response2.body[0].title).toBe(languageComplete.title);

    });

    it('can be find by id correctly', async () => {
        languageComplete.userId = user.id;
        expect(user.username).toBe(userComplete.username);

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(languageComplete);

        expect(response.status).toBe(200);
        expect(response.body.title).toBe(languageComplete.title);
        expect(response.body.speak).toBe(languageComplete.speak);
        expect(response.body.read).toBe(languageComplete.read);
        expect(response.body.understand).toBe(languageComplete.understand);

        const newLanguage = response.body;

        const response2 = await request.get(`${url}/${newLanguage.id}`)
            .set('Authorization', dbHandle.header)
            .send();

        expect(response2.status).toBe(200);
        expect(response2.body.title).toBe(languageComplete.title);

    });

    it('can be delete correctly', async () => {
        languageComplete.userId = user.id;
        expect(user.username).toBe(userComplete.username);

        const response = await request.post(url)
            .set('Authorization', dbHandle.header)
            .send(languageComplete);

        expect(response.status).toBe(200);
        expect(response.body.title).toBe(languageComplete.title);

        const newLanguage = response.body;

        const response2 = await request.delete(`${url}/${newLanguage.id}`)
            .set('Authorization', dbHandle.header)
            .send();
        expect(response2.status).toBe(200);
        expect(response2.body.message).toBe('deleted');

    });
});


const languageComplete = {
    title: 'en',
    speak: 3,
    understand: 2,
    read: 4,
    userId: 1
};

const userComplete = {
    email: 'danick.takam@datnek.be',
    username: "stratege",
    password: "password",
    slug: '113'
};

```

- Créer le fichier languages.controller.js
```js
touch controllers/languages.controller.js
```

- Modifier le contenu du fichier crée
```js
'use strict';



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

```

# VI. Créer les controlleurs

- Créer le fichier users.controller.js
```js
touch controllers/users.controller.js
```


- Modifier le contenu
```js
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

```

# VII. Routes

- Créer le dossier app:  ce dossier contiendra la logique de notre application
```js
mkdir app
```

- Créer le fichier index.js dans le repertoire app
```js
touch app/index.js
```

- Remplacer le contenu
```js
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

```

- Créer le fichier auth.grade.js dans le repertoire app: ce fichier permettra de gérer la sécurité de l'application
```js
touch app/auth.grade.js
```

- Remplacer le contenu du fichier
```js
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

```


- Créer le dossier routes pour gerer les routes de l'application
```js
mkdir app/routes
```

- Créer le fichier de route users.route.js
```js
touch app/routes/users.route.js
```


- Changer le contenu
```js
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

```



- Créer le fichier de route languages.route.js
```js
touch app/routes/languages.route.js
```


- Changer le contenu
```js
'use strict';

module.exports = (routes, db, authenticateToken) => {
    const LanguageController = require('../../controllers/languages.controller')(db);
    routes.get('/languages', authenticateToken, LanguageController.findAll);
    routes.get('/languages/:id', authenticateToken, LanguageController.findById);
    routes.post('/languages', authenticateToken, LanguageController.create);
    routes.delete('/languages/:id', authenticateToken, LanguageController.delete);
    routes.put('/languages/:id', authenticateToken, LanguageController.put);
};

```

- Créer le fichier de route index.js
```js
touch app/routes/index.js
```


- Changer le contenu
```js
'use strict';

module.exports = (routes, db, authGrade) => {
    // language all routes
    require('./languages.route')(routes, db, authGrade.authenticateToken);
    require('./users.route')(routes, db, authGrade);
    // rest
};
```

- Créer le fichier server.js à la racine du projet
```js
touch server.js
```


- Remplacer le contenu 
```js
'use strict';
// process.env.NODE_ENV = 'development';
const app = require('./app/index');

if (process.env.NODE_ENV !== 'test') {
    app.listen(process.env.NODE_PORT || 3000, () => {
        console.log(`Server is up on port ${process.env.NODE_PORT || 3000}`);
    });
}

module.exports = app;
```

- Exécuter le test unitaire
```js
npm test
```

- Tester l'application
```js
npm start
```
