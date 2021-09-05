CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS blogs(
  id INT NOT NULL primary key  AUTO_INCREMENT COMMENT 'blog id',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title varchar(255) COMMENT 'Blog Title',
  body varchar(255) COMMENT 'Blog Body',
  imgUrl varchar(255) COMMENT 'Blog Image',
  published TINYINT COMMENT 'Blog Published',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account Id of Creator',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'blog id',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  body varchar(255) COMMENT 'Blog Body',
  blogId INT NOT NULL COMMENT 'Blog ID',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account Id of Creator',

  FOREIGN KEY (blogId) REFERENCES blogs(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';