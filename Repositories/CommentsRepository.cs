using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Repositories
{
  public class CommentsRepository
  {

    private readonly IDbConnection _dataBase;
    public CommentsRepository(IDbConnection dataBase)
    {
      _dataBase = dataBase;
    }
    public List<Comment> GetBlogComment(int blogId)
    {
      string sql = @" SELECT * FROM comments c WHERE c.blogId = @blogId";
      return _dataBase.Query<Comment>(sql, new { blogId }).ToList();
    }

    public Comment Post(Comment commentData)
    {
      var sql = @"
      INSERT INTO comments(
        body,
        blog,
        creatorId
      )
      VALUES(
        @Body,
        @Blog,
        @CreatorId
      );
      SELECT LAST_INSERT_ID();
      ";
      var id = _dataBase.ExecuteScalar<int>(sql, commentData);
      commentData.Id = id;
      return commentData;
    }

    public Comment Edit(int commentId, Comment commentData)
    {
      commentData.Id = commentId;
      var sql = @"
      UPDATE comments
      SET
      body = @Body,
    WHERE id = @Id
      ";
      var rowsAffected = _dataBase.Execute(sql, commentData);
      if (rowsAffected > 1)
      {
        throw new System.Exception("SOmething is wrong");
      }
      if (rowsAffected == 0)
      {
        throw new System.Exception("Edit Failed");
      }
      return commentData;
    }

    public Comment GetById(int commentId)
    {
      return _dataBase.QueryFirstOrDefault<Comment>("SELECT * FROM artists WHERE id = @id", new { commentId });
    }

    public void Delete(int commentId)
    {
      var rowsAffected = _dataBase.Execute("DELETE FROM comments WHERE id = @id", new { commentId });
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