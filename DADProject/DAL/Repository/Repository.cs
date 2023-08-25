using DAL.Abstractions.Repository;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace DAL.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _table;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            Log.Information($"Getting all entities from table {typeof(T).Name}");
            var resultCollection = await _table.ToListAsync();

            Log.Information("All entities have been returned.");
            return resultCollection;
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to get all entitied from ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task<T> GetByIdAsync(object id)
    {
        try
        {
            Log.Information($"Getting entity with id {id} from table {typeof(T).Name}");
            var result = await _table.FindAsync(id);

            Log.Information("Entity has been returned.");
            return result;
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to get entity with id {id} from ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task InsertAsync(T obj)
    {
        try
        {
            Log.Information($"Inserting entity into table {typeof(T).Name}");
            await _table.AddAsync(obj);
            await _context.SaveChangesAsync();

            Log.Information("Entity has been inserted.");
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to insert entity into ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task InsertRangeAsync(IEnumerable<T> obj)
    {
        try
        {
            Log.Information($"Inserting multiple entities into table {typeof(T).Name}");
            await _table.AddRangeAsync(obj);
            await _context.SaveChangesAsync();

            Log.Information("Entities have been inserted.");
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to insert multiple entities into ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task UpdateAsync(T obj)
    {
        try
        {
            Log.Information($"Updating entity in table {typeof(T).Name}");
            _table.Update(obj);
            await _context.SaveChangesAsync();

            Log.Information("Entity has been updated.");
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to update entity in ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task DeleteAsync(object id)
    {
        try
        {
            Log.Information($"Deleting entity with id {id} from table {typeof(T).Name}");
            var entity = await _table.FindAsync(id);
            
            if (entity != null)
                _table.Remove(entity);

            await _context.SaveChangesAsync();

            Log.Information("Entity has been deleted.");
        }
        catch (Exception e)
        {
            Log.Fatal(e, $"Error when trying to delete entity with id {id} from ${typeof(T).Name}");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public async Task DeleteAllAsync()
    {
       try
       {
           Log.Information($"Deleting all entities from table {typeof(T).Name}");
           _table.RemoveRange(_table);

           await _context.SaveChangesAsync();

           Log.Information("All entities have been deleted.");
       }
       catch (Exception e)
       {
           Log.Fatal(e, $"Error when trying to delete all entities from ${typeof(T).Name}");
           throw;
       }
       finally
       {
           Log.CloseAndFlush();
       }
    }

    public async Task SaveAsync()
    {
       try
       {
           Log.Information($"Saving changes to table {typeof(T).Name}");
           await _context.SaveChangesAsync();

           Log.Information("Changes have been saved.");
       }
       catch (Exception e)
       {
           Log.Fatal(e, $"Error when trying to save changes to ${typeof(T).Name}");
           throw;
       }
       finally
       {
           Log.CloseAndFlush();
       }
    }
}