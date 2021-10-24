using System;
using System.Collections.Generic;
using Bloggr.Models;
using Bloggr.Repositories;

namespace Bloggr.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _blogsRepository;
    public BlogsService(BlogsRepository blogsRepository)
    {
      _blogsRepository = blogsRepository;
    }
    public List<Blog> GetAll()
    {
      return _blogsRepository.GetAll();
    }
    public Blog GetById(int blogId)
    {
      Blog foundBlog = _blogsRepository.GetById(blogId);
      if (foundBlog == null)
      {
        throw new Exception("Can't find the blog");
      }
      return foundBlog;
    }
    public Blog Post(Blog blogData)
    {
      return _blogsRepository.Post(blogData);
    }
    public Blog Edit(int blogId, Blog blogData)
    {
      var blog = GetById(blogId);
      blog.Title = blogData.Title ?? blog.Title;
      blog.Body = blogData.Body ?? blog.Body;
      blog.ImgUrl = blogData.ImgUrl ?? blog.ImgUrl;
      blog.Published = blogData.Published;
      _blogsRepository.Edit(blogId, blogData);
      return blog;
    }
    public void Delete(int blogId, string userId)
    {
      Blog blog = GetById(blogId);
      if (blog.CreatorId != userId)
      {
        throw new Exception("not authorized");
      }
      _blogsRepository.Delete(blogId);
    }

    public List<Blog> GetBlogByAccount(string userId)
    {
      return _blogsRepository.GetBlogByAccount(userId);
    }
  }
}