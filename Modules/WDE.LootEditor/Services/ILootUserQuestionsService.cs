using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WDE.Common.Database;
using WDE.Common.Services.MessageBox;
using WDE.Module.Attributes;

namespace WDE.LootEditor.Services;

[UniqueProvider]
internal interface ILootUserQuestionsService
{
    Task<SaveDialogResult> AskToSave(
        LootSourceType sourceType,
        ICollection<LootEntry> referencesToBeUnloaded,
        ICollection<LootEntry> rootsToBeRemoved);
    Task NotifyCouldNotApplySql(Exception error);
}