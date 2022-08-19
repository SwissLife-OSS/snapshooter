using System;
using System.Linq;
using System.Linq.Expressions;
using Snapshooter.Helpers;

namespace Snapshooter.MatchOptionsOfModel.cs;

/// <summary>
/// <see cref="MatchOptions"/> which also knows the type of the object we assert against.
/// </summary>
public class MatchOptions<TModel> : MatchOptions
{
    /// <summary>
    /// Constructor of the <see cref="MatchOptions{TModel}"/> class to create
    /// a new instance.
    /// </summary>
    public MatchOptions(MatchOptions predecessor)
    {
        _matchOperators = predecessor.MatchOperators.ToList();
    }

    /// <summary>
    /// The <see cref="AcceptField{TU}"/> match option accepts a
    /// lambda expression pointing to a field. The value of the field will NOT be 
    /// compared with the original snapshot.
    /// In addition, the field will be replaced by the text 'AcceptAny{T}()'"
    /// </summary>
    public MatchOptions<TModel> AcceptField<TU>(Expression<Func<TModel, TU>> fieldPath)
    {
        var path = LambdaPath<TModel>.Get(fieldPath);
        Accept<TU>(path);

        return this;
    }


    /// <summary>
    /// The <see cref="IgnoreFields{TU}"/> option ignores the existing field(s) given by the 
    /// lambda expression. The field(s) will be ignored during snapshot comparison.
    /// </summary>
    public MatchOptions<TModel> IgnoreFields<TU>(Expression<Func<TModel, TU>> fieldPaths)
    {
        var path = LambdaPath<TModel>.Get(fieldPaths);
        IgnoreFields<TU>(path);

        return this;
    }
}
