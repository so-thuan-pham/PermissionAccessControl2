﻿// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAuthorize
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// This is called in the overridden SaveChanges in the application's DbContext
        /// Its job is to see if a entity has a IUserId or ITenantKey and set the appropriate key 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="accessKey"></param>
        public static void MarkWithDataKeyIfNeeded(this DbContext context, string accessKey)
        {
            foreach (var entityEntry in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entityEntry.Entity is IHierarchicalKey hasHierarchicalKey)
                    hasHierarchicalKey.SetHierarchicalDataKey(accessKey);
            }
        }
    }
}