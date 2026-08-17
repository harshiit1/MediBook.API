using Medicare.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Interfaces.ISecurityQuestionsRepository
{
    public interface ISecurityQuestionRepository
    {
        Task<List<SecurityQuestionDataModel>> GetSecurityQuestionMasterAsync();
    }
}
