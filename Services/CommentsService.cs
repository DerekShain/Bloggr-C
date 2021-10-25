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
      Comment comment = GetById(commentId);
      if (comment.CreatorId != comment.CreatorId)
      {
        throw new Exception("Not Allowed");
      }
      comment.Body = commentData.Body ?? comment.Body;
      comment.Blog = commentData.Blog;
      comment.Title = commentData.Title ?? comment.Title;
      comment.Published = commentData.Published;
      return _commentsRepository.Edit(commentId, commentData);
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

    public void Delete(int commentId, string userId)
    {
      Comment comment = GetById(commentId);
      if (comment.CreatorId != userId)
      {
        throw new Exception("not authorized");
      }
      _commentsRepository.Delete(commentId);
    }

    public List<Comment> GetCommentByProfile(string profileId)
    {
      return _commentsRepository.GetCommentByProfile(profileId);
    }

    public List<Comment> GetCommentByAccount(string userId)
    {
      return _commentsRepository.GetCommentByAccount(userId);
    }
  }
}