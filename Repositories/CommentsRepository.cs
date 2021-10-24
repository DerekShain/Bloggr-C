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
    internal Comment Post(Comment commentData)
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
    internal Comment GetById(int commentId)
    {
      var sql = @"
      SELECT 
      *
      FROM comments
      WHERE id = @commentId
      ";
      return _dataBase.Query<Comment>(sql, new { commentId }).FirstOrDefault();
    }
    internal List<Comment> GetBlogComment(int blogId)
    {
      string sql = @" 
      SELECT 
      *
      FROM comments c WHERE c.blog = @blogId";
      return _dataBase.Query<Comment>(sql, new { blogId }).ToList();
    }
    internal Comment Edit(int commentId, Comment commentData)
    {
      commentData.Id = commentId;
      string sql = @"
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
    internal List<Comment> GetCommentByAccount(string userId)
    {
      string sql = @"
      SELECT
      *
      FROM comments c
      WHERE c.creatorId = @userId
      ";
      return _dataBase.Query<Comment>(sql, new { userId }).ToList();
    }
    internal void Delete(int commentId)
    {
      var sql = @"
      DELETE
      FROM comments
      WHERE id = @commentId LIMIT 1
      ";
      var rowsAffected = _dataBase.Execute(sql, new { commentId });
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