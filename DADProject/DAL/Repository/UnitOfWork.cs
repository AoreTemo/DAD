using Core.Models;
using DAL.Data;

namespace DAL.Repository;

public class UnitOfWork : IDisposable
{
    private ApplicationDbContext     _context;
    private Repository<AppUser>      _appUserRepository;
    private Repository<Comment>      _commentRepository;
    private Repository<Media>        _mediaRepository;
    private Repository<Message>      _messageRepository;
    private Repository<Notification> _notificaionRepository;
    private Repository<Post>         _postRepository;
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Repository<AppUser> AppUserRepository
    {
        get
        {
            if (_appUserRepository == null)
            {
                _appUserRepository = new Repository<AppUser>(_context);
            } 
            
            return _appUserRepository;
        }
    }

    public Repository<Comment> CommentRepository
    {
        get
        {
            if (_commentRepository == null)
            {
                _commentRepository = new Repository<Comment>(_context);
            } 
            
            return _commentRepository;
        }
    }

    public Repository<Media> MediaRepository
    {
        get
        {
            if (_mediaRepository == null)
            {
                _mediaRepository = new Repository<Media>(_context);
            } 
            
            return _mediaRepository;
        }
    }

    public Repository<Message> MessageRepository
    {
        get
        {
            if (_messageRepository == null)
            {
                _messageRepository = new Repository<Message>(_context);
            } 
            
            return _messageRepository;
        }
    }

    public Repository<Notification> NotificaionRepository
    {
        get
        {
            if (_notificaionRepository == null)
            {
                _notificaionRepository = new Repository<Notification>(_context);
            } 
            
            return _notificaionRepository;
        }
    }

    public Repository<Post> PostRepository
    {
        get
        {
            if (_postRepository == null)
            {
                _postRepository = new Repository<Post>(_context);
            } 
            
            return _postRepository;
        }
    }

    
    protected virtual async Task Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            await _context.DisposeAsync();
        }

        _disposed = true;
    }

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }
}