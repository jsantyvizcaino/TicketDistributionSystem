using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.UpdateUserWorkload;

/// <summary>
/// Comando para actualizar los contadores de carga de trabajo de un usuario
/// (ítems asignados, pendientes, de alta relevancia y finalización de un ítem).
/// Es invocado por MsItem para reflejar el efecto de la distribución/finalización de ítems.
/// </summary>
public sealed record UpdateUserWorkloadCommand(
    string Username,
    int IncrementAssigned,
    int IncrementPending,
    int IncrementHighRelevance,
    bool IsCompletion
) : ICommand<UserResponse>;
