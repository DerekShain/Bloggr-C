CREATE TABLE IF NOT EXISTS profiles(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  creatorId varchar(255) COMMENT 'CreatorId',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS blogs(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title varchar(255) COMMENT 'title',
  body varchar(5000) COMMENT 'body',
  imgUrl varchar(255) COMMENT ' imgurl',
  published TINYINT COMMENT 'published',
  creatorId varchar(255) COMMENT 'CreatorId',
   FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  body varchar(5000) COMMENT 'BODY',
  creatorId varchar(255) COMMENT 'CreatorId',
  blog INT COMMENT 'BLOG',
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(blog) REFERENCES blogs(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';