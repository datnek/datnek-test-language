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
