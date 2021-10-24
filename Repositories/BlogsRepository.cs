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
      return _dataBase.Query<Blog>(@"
      SELECT 
      * 
      FROM blogs").ToList();
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
      // if (rowsAffected == 0)
      // {
      //   throw new System.Exception("Edit Failed");
      // }
      return blogData;
    }

    internal void Delete(int blogId)
    {
      var rowsAffected = _dataBase.Execute("DELETE FROM blogs WHERE id = @blogId LIMIT 1", new { blogId });
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