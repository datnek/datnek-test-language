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
