using System;

namespace FrankenToilet.mercy;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class MercyFeatureAttribute : Attribute;