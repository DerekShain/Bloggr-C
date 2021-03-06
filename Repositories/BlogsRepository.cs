using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _dataBase;
    public BlogsRepository(IDbConnection dataBase)
    {
      _dataBase = dataBase;
    }
    internal Blog Post(Blog blogData)
    {
      var sql = @"
      INSERT INTO blogs(
        title,
        body,
        imgUrl,
        published,
        creatorId
      )
      VALUES(
        @Title,
        @Body,
        @ImgUrl,
        @Published,
        @CreatorId
      );
     SELECT LAST_INSERT_ID();
      ";
      int id = _dataBase.ExecuteScalar<int>(sql, blogData);
      blogData.Id = id;
      return blogData;
    }

    internal Blog GetById(int blogId)
    {
      string sql = @"
      SELECT 
      b.*,
      a.* 
      FROM blogs b
      JOIN accounts a on a.id= b.creatorId
      WHERE b.id = @blogId;
      ";
      return _dataBase.Query<Blog, Account, Blog>(sql, (b, a) =>
      {
        b.Creator = a;
        return b;
      }, new { blogId }).FirstOrDefault();
    }

    internal List<Blog> GetAll()
    {
      var sql = @"
      SELECT
      *
      FROM blogs
      Where published = true
      ";
      return _dataBase.Query<Blog>(sql).ToList();
    }
    internal Blog Edit(int blogId, Blog blogData)
    {
      blogData.Id = blogId;
      var sql = @"
      UPDATE blogs
      SET
      title = @Title,
      body = @Body,
      imgUrl = @ImgUrl,
      published = @Published
      WHERE id = @Id
      ";
      var rowsAffected = _dataBase.Execute(sql, blogData);
      if (rowsAffected > 1)
      {
        throw new System.Exception("SOmething is wrong");
      }
      return blogData;
    }

    internal List<Blog> GetBlogByProfile(string profileId)
    {
      string sql = @"
      SELECT
      *
      FROM blogs b
      WHERE b.creatorId = @profileId AND published = true
      ";
      return _dataBase.Query<Blog>(sql, new { profileId }).ToList();
    }

    internal List<Blog> GetBlogByAccount(string userId)
    {
      string sql = @"
      SELECT
      *
      FROM blogs b
      WHERE b.creatorId = @userId
      ";
      return _dataBase.Query<Blog>(sql, new { userId }).ToList();
    }
    internal void Delete(int blogId)
    {
      var sql = @"
      DELETE
      FROM blogs
      WHERE id = @blogId LIMIT 1
      ";
      var rowsAffected = _dataBase.Execute(sql, new { blogId });
      if (rowsAffected > 1)
      {
        throw new System.Exception("Something is wrong");
      }
      if (rowsAffected == 0)
      {
        throw new System.Exception("Delete Failed");
      }
    }
  }
}