'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.addColumn('Languages', "userId",{
      allowNull: false,
      type: Sequelize.INTEGER
    });

    await queryInterface.addColumn('Languages', "slug",{
      allowNull: false,
      type: Sequelize.STRING
    });
  },
  down: async (queryInterface, Sequelize) => {
    await queryInterface.removeColumn('Languages', "userId");
    await queryInterface.removeColumn('Languages', "slug");
  }
};
