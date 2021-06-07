using Mongo.Dal.Extensions;
using Mongo.Dal.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mongo.Dal
{
    /// <summary>
    /// The mongo db.
    /// </summary>
    public class MongoDb : IMongoDatabase
    {
        private static string cnt;
        private static string db;
        private static MongoClient c;
        private readonly IMongoDatabase d;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDb"/> class.
        /// </summary>
        public MongoDb(IMongoClient client, IDatabaseSettings databaseSettings)
        {
            if (c == null)
            {
                if (string.IsNullOrEmpty(cnt))
                {
                    cnt = databaseSettings.ConnectionString;
                }

                c = (MongoClient)client;
            }

            if (this.d == null)
            {
                if (string.IsNullOrEmpty(db))
                {
                    db = databaseSettings.DatabaseName;
                }

                this.d = c.GetDatabase(db);
            }
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        public IMongoClient Client => this.d.Client;

        /// <summary>
        /// Gets the database namespace.
        /// </summary>
        public DatabaseNamespace DatabaseNamespace => this.d.DatabaseNamespace;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public MongoDatabaseSettings Settings => this.d.Settings;

        /// <summary>
        /// Aggregates the.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.Aggregate<TResult>(pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Aggregates the.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session, PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.Aggregate<TResult>(session, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Aggregates the async.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.AggregateAsync<TResult>(pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Aggregates the async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session, PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.AggregateAsync<TResult>(session, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void CreateCollection(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            this.d.CreateCollection(name, options, cancellationToken);
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void CreateCollection(IClientSessionHandle session, string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            this.d.CreateCollection(session, name, options, cancellationToken);
        }

        /// <summary>
        /// Creates the collection async.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task CreateCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.CreateCollectionAsync(name, options, cancellationToken);
        }

        /// <summary>
        /// Creates the collection async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task CreateCollectionAsync(IClientSessionHandle session, string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.CreateCollectionAsync(session, name, options, cancellationToken);
        }

        /// <summary>
        /// Creates the view.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="viewOn">The view on.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <typeparam name="TResult">The type1.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void CreateView<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default)
        {
            this.d.CreateView<TDocument, TResult>(viewName, viewOn, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Creates the view.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="viewOn">The view on.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <typeparam name="TResult">The type1.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void CreateView<TDocument, TResult>(IClientSessionHandle session, string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default)
        {
            this.d.CreateView<TDocument, TResult>(session, viewName, viewOn, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Creates the view async.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="viewOn">The view on.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <typeparam name="TResult">The type1.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task CreateViewAsync<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default)
        {
            return this.d.CreateViewAsync<TDocument, TResult>(viewName, viewOn, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Creates the view async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="viewOn">The view on.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <typeparam name="TResult">The type1.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task CreateViewAsync<TDocument, TResult>(IClientSessionHandle session, string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default)
        {
            return this.d.CreateViewAsync<TDocument, TResult>(session, viewName, viewOn, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Drops the collection.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void DropCollection(string name, CancellationToken cancellationToken = default)
        {
            this.d.DropCollection(name, cancellationToken);
        }

        /// <summary>
        /// Drops the collection.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void DropCollection(IClientSessionHandle session, string name, CancellationToken cancellationToken = default)
        {
            this.d.DropCollection(session, name, cancellationToken);
        }

        /// <summary>
        /// Drops the collection async.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task DropCollectionAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.d.DropCollectionAsync(name, cancellationToken);
        }

        /// <summary>
        /// Drops the collection async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task DropCollectionAsync(IClientSessionHandle session, string name, CancellationToken cancellationToken = default)
        {
            return this.d.DropCollectionAsync(session, name, cancellationToken);
        }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <returns>A long.</returns>
        public long GetTotalCount<TDocument>()
            where TDocument : new()
        {
            return this.GetCollection<TDocument>().CountDocuments(x => true);
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="settings">The settings.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <returns>An IMongoCollection.</returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name, MongoCollectionSettings settings = null)
        {
            return this.d.GetCollection<TDocument>(name, settings);
        }

        /// <summary>
        /// Lists the collection names.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<string> ListCollectionNames(ListCollectionNamesOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionNames(options, cancellationToken);
        }

        /// <summary>
        /// Lists the collection names.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<string> ListCollectionNames(IClientSessionHandle session, ListCollectionNamesOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionNames(session, options, cancellationToken);
        }

        /// <summary>
        /// Lists the collection names async.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<string>> ListCollectionNamesAsync(ListCollectionNamesOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionNamesAsync(options, cancellationToken);
        }

        /// <summary>
        /// Lists the collection names async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<string>> ListCollectionNamesAsync(IClientSessionHandle session, ListCollectionNamesOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionNamesAsync(session, options, cancellationToken);
        }

        /// <summary>
        /// Lists the collections.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<BsonDocument> ListCollections(ListCollectionsOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollections(options, cancellationToken);
        }

        /// <summary>
        /// Lists the collections.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An IAsyncCursor.</returns>
        public IAsyncCursor<BsonDocument> ListCollections(IClientSessionHandle session, ListCollectionsOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollections(session, options, cancellationToken);
        }

        /// <summary>
        /// Lists the collections async.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<BsonDocument>> ListCollectionsAsync(ListCollectionsOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionsAsync(options, cancellationToken);
        }

        /// <summary>
        /// Lists the collections async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task<IAsyncCursor<BsonDocument>> ListCollectionsAsync(IClientSessionHandle session, ListCollectionsOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.ListCollectionsAsync(session, options, cancellationToken);
        }

        /// <summary>
        /// Renames the collection.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void RenameCollection(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            this.d.RenameCollection(oldName, newName, options, cancellationToken);
        }

        /// <summary>
        /// Renames the collection.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void RenameCollection(IClientSessionHandle session, string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            this.d.RenameCollection(session, oldName, newName, options, cancellationToken);
        }

        /// <summary>
        /// Renames the collection async.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task RenameCollectionAsync(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.RenameCollectionAsync(oldName, newName, options, cancellationToken);
        }

        /// <summary>
        /// Renames the collection async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task RenameCollectionAsync(IClientSessionHandle session, string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.RenameCollectionAsync(session, oldName, newName, options, cancellationToken);
        }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readPreference">The read preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A TResult.</returns>
        public TResult RunCommand<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default)
        {
            return this.d.RunCommand<TResult>(command, readPreference, cancellationToken);
        }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="command">The command.</param>
        /// <param name="readPreference">The read preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A TResult.</returns>
        public TResult RunCommand<TResult>(IClientSessionHandle session, Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default)
        {
            return this.d.RunCommand<TResult>(session, command, readPreference, cancellationToken);
        }

        /// <summary>
        /// Runs the command async.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readPreference">The read preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<TResult> RunCommandAsync<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default)
        {
            return this.d.RunCommandAsync<TResult>(command, readPreference, cancellationToken);
        }

        /// <summary>
        /// Runs the command async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="command">The command.</param>
        /// <param name="readPreference">The read preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<TResult> RunCommandAsync<TResult>(IClientSessionHandle session, Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default)
        {
            return this.d.RunCommandAsync<TResult>(session, command, readPreference, cancellationToken);
        }

        /// <summary>
        /// Watches the.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>An IChangeStreamCursor.</returns>
        public IChangeStreamCursor<TResult> Watch<TResult>(PipelineDefinition<ChangeStreamDocument<BsonDocument>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.Watch<TResult>(pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Watches the.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>An IChangeStreamCursor.</returns>
        public IChangeStreamCursor<TResult> Watch<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<BsonDocument>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.Watch<TResult>(session, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Watches the async.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(PipelineDefinition<ChangeStreamDocument<BsonDocument>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.WatchAsync<TResult>(pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Watches the async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResult">The type.</typeparam>
        /// <returns>A Task.</returns>
        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<BsonDocument>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return this.d.WatchAsync<TResult>(session, pipeline, options, cancellationToken);
        }

        /// <summary>
        /// Withs the read concern.
        /// </summary>
        /// <param name="readConcern">The read concern.</param>
        /// <returns>An IMongoDatabase.</returns>
        public IMongoDatabase WithReadConcern(ReadConcern readConcern)
        {
            return this.d.WithReadConcern(readConcern);
        }

        /// <summary>
        /// Withs the read preference.
        /// </summary>
        /// <param name="readPreference">The read preference.</param>
        /// <returns>An IMongoDatabase.</returns>
        public IMongoDatabase WithReadPreference(ReadPreference readPreference)
        {
            return this.d.WithReadPreference(readPreference);
        }

        /// <summary>
        /// Withs the write concern.
        /// </summary>
        /// <param name="writeConcern">The write concern.</param>
        /// <returns>An IMongoDatabase.</returns>
        public IMongoDatabase WithWriteConcern(WriteConcern writeConcern)
        {
            return this.d.WithWriteConcern(writeConcern);
        }

        /// <summary>
        /// Collections the exists async.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>A Task.</returns>
        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);

            // filter by collection name
            var collections = await this.d.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            // check for existence
            return await collections.AnyAsync();
        }

        /// <summary>
        /// Collections the exists.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>A bool.</returns>
        public bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return this.d.ListCollectionNames(options).Any();
        }

        /// <summary>
        /// Aggregates the to collection.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void AggregateToCollection<TResult>(PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Aggregates the to collection.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void AggregateToCollection<TResult>(IClientSessionHandle session, PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Aggregates the to collection async.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task AggregateToCollectionAsync<TResult>(PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Aggregates the to collection async.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public Task AggregateToCollectionAsync<TResult>(IClientSessionHandle session, PipelineDefinition<NoPipelineInput, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
