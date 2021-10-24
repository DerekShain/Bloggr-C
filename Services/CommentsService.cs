using System;
using System.Collections.Generic;
using Bloggr.Models;
using Bloggr.Repositories;

namespace Bloggr.Services
{
  public class CommentsService
  {
    private readonly CommentsRepository _commentsRepository;
    public CommentsService(CommentsRepository commentsRepository)
    {
      _commentsRepository = commentsRepository;
    }
    public List<Comment> GetBlogComment(int blogId)
    {
      return _commentsRepository.GetBlogComment(blogId);
    }

    public Comment Post(Comment commentData)
    {
      return _commentsRepository.Post(commentData);
    }

    public Comment Edit(int commentId, Comment commentData)
    {
      var comment = GetById(commentId);
      comment.Body = commentData.Body ?? comment.Body;
      comment.Blog = commentData.Blog;
      _commentsRepository.Edit(commentId, commentData);
      return comment;
    }

    public Comment GetById(int commentId)
    {
      Comment foundComment = _commentsRepository.GetById(commentId);
      if (foundComment == null)
      {
        throw new Exception("Can't find Comment");
      }
      return foundComment;
    }

    public Comment Delete(int commentId)
    {
      var comment = GetById(commentId);
      _commentsRepository.Delete(commentId);
      return comment;
    }
  }
}