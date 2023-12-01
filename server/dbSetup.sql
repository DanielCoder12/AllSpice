CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(100) NOT NULL,
        instructions VARCHAR(1000) NOT NULL,
        img VARCHAR(500) NOT NULL,
        category VARCHAR(100) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE recipes;

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name VARCHAR(500) NOT NULL,
        quantity VARCHAR(100) NOT NULL,
        recipeId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE ingredients;

SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();

CREATE TABLE
    IF NOT EXISTS favorites (
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        UNIQUE(accountId, recipeId)
    ) default charset utf8 COMMENT '';

DROP TABLE favorites;

SELECT fav.*, acc.*
FROM favorites fav
    JOIN accounts acc ON fav.accountId = acc.id
WHERE
    fav.accountId = "6541814359b43990cb08b854";

SELECT * FROM favorites

SELECT fav.*, rep.*, acc.*
FROM favorites fav
    JOIN recipes rep ON rep.id = fav.recipeId
    JOIN accounts acc ON acc.id = rep.creatorId
WHERE
    fav.accountId = "6541814359b43990cb08b854";