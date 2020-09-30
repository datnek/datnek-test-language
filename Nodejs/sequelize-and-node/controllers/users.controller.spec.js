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
