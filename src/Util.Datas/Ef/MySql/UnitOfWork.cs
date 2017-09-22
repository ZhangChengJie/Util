﻿using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Util.Datas.Ef.Core;
using System.Data.Common;
using Util.Datas.Ef.Internal;
using Util.Datas.UnitOfWorks;

namespace Util.Datas.Ef.MySql {
    /// <summary>
    /// MySql工作单元
    /// </summary>
    public abstract class UnitOfWork: UnitOfWorkBase {
        /// <summary>
        /// 初始化MySql工作单元
        /// </summary>
        /// <param name="connection">连接字符串</param>
        /// <param name="manager">工作单元服务</param>
        protected UnitOfWork( string connection, IUnitOfWorkManager manager = null )
            : base( new DbContextOptionsBuilder().UseMySql( connection ).Options, manager ) {
        }

        /// <summary>
        /// 初始化MySql工作单元
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="manager">工作单元服务</param>
        protected UnitOfWork( DbConnection connection, IUnitOfWorkManager manager = null )
            : base( new DbContextOptionsBuilder().UseMySql( connection ).Options, manager ) {
        }

        /// <summary>
        /// 初始化MySql工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="manager">工作单元服务</param>
        protected UnitOfWork( DbContextOptions options, IUnitOfWorkManager manager = null )
            : base( options, manager ) {
        }

        /// <summary>
        /// 获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        protected override IEnumerable<Util.Datas.Ef.Core.IMap> GetMapTypes( Assembly assembly ) {
            return Util.Helpers.Reflection.GetTypesByInterface<IMap>( assembly );
        }

        /// <summary>
        /// 拦截添加操作
        /// </summary>
        protected override void InterceptAddedOperation( EntityEntry entry ) {
            base.InterceptAddedOperation( entry );
            Helper.InitVersion( entry );
        }

        /// <summary>
        /// 拦截修改操作
        /// </summary>
        protected override void InterceptModifiedOperation( EntityEntry entry ) {
            base.InterceptModifiedOperation( entry );
            Helper.InitVersion( entry );
        }
    }
}
