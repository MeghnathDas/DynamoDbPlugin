﻿/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.Core.DynamoDb
{
    using System;
    internal class DbTableAttributeInfo
    {
        public string name { get; set; }
        public Type dataType { get; set; }
        public bool isPrimary { get; set; }
        public bool isRange { get; set; }
    }
}
