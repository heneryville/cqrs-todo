import { Injector } from "@angular/core";
import { LocalTodoState } from "./local-todo-state";
import { Todo } from "./todo";

export interface ICommand {
  type: string;
  id: string;
  [key: string]: any;
}

export interface ITodoCreateCommand extends ICommand { title: string };
export interface ITodoToggleCompleteCommand extends ICommand { priorCompleteState: boolean };

export interface ICommandHandler<T extends ICommand> {
  isOnlineOnly: boolean;
  imitate(cmd: T, injector: Injector);
}

export const todoHandlers: { [key: string]: ICommandHandler<any> } = {
  'Todo.Create': {
    isOnlineOnly: false,
    imitate(cmd: ITodoCreateCommand, injector: Injector) {
      injector.get(LocalTodoState).addTodo(new Todo({ id: cmd.id, title: cmd.title }));
    }
  },
  'Todo.ToggleComplete': {
    isOnlineOnly: false,
    imitate(cmd: ITodoToggleCompleteCommand, injector: Injector) {
      injector.get(LocalTodoState).updateTodoById(cmd.id, { isCompleted: !cmd.priorCompleteState });
    }
  },
  'Todo.Delete': {
    isOnlineOnly: false,
    imitate(cmd: ITodoToggleCompleteCommand, injector: Injector) {
      injector.get(LocalTodoState).deleteTodoById(cmd.id);
    }
  }
};
