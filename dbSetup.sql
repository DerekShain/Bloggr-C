CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS blogs(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title varchar(255) COMMENT 'title',
  body varchar(5000) COMMENT 'body',
  imgUrl varchar(255) COMMENT ' imgurl',
  published TINYINT COMMENT 'published',
  creatorId varchar(255) COMMENT 'CreatorId'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
  body varchar(255) COMMENT 'BODY',
 creatorId varchar(255) COMMENT 'CreatorId',
  blogId INT COMMENT 'BLOG'
) default charset utf8 COMMENT '';