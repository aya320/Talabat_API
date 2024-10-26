﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Common;

namespace Talabat.Infrastructure.Persistence.Data.Interceptors
{
	public class CustomSaveChangesInterceptors :SaveChangesInterceptor
	{
		private readonly ILoggedInUserService _loggedInUserService;

		public CustomSaveChangesInterceptors(ILoggedInUserService loggedInUserService)
		{
			_loggedInUserService = loggedInUserService;
		}

		public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
		{
			UpdatedEntities(eventData.Context);
			return base.SavedChanges(eventData, result);
		}
		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
			UpdatedEntities(eventData.Context);
			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}

		private void UpdatedEntities(DbContext? dbContext)
		{
			if (dbContext == null)
				return;
			foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>().Where(entity => entity.State is EntityState.Added or EntityState.Modified))
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;
				}

				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;
			}
		}
	}
}
