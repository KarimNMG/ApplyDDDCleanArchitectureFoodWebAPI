using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Common;

public interface IJwtTokenJenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}