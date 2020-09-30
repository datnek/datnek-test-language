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
