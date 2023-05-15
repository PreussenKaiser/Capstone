namespace Scheduler.Filters;

/// <summary>
/// Marker attribute for ignoring the ChangePasswordFIlter.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class IgnoreChangePasswordAttribute : Attribute
{
}
