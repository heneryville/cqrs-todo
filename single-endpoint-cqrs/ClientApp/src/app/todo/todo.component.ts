import { Component } from '@angular/core';
import { Todo } from '../todo';
import { LocalTodoState } from '../local-todo-state';
import { CommandBuffer } from '../command-buffer';

@Component({
  selector: 'app-root',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
})
export class TodoComponent {

  newTodo: Todo = new Todo();

  constructor(private todoState: LocalTodoState, private cmd: CommandBuffer) {
    todoState.fetch();
  }

  addTodo() {
    this.cmd.issueCommand({
      type: 'Todo.Create',
      id: uuidv4(),
      title: this.newTodo.title,
    })
    this.newTodo = new Todo();
  }

  toggleTodoComplete(todo) {
    this.cmd.issueCommand({
      type: 'Todo.ToggleComplete',
      id: todo.id,
      priorCompleteState: todo.isCompleted,
    });
  }

  removeTodo(todo) {
    this.cmd.issueCommand({
      type: 'Todo.Delete',
      id: todo.id,
    });
  }

  get todos() {
    return this.todoState.getAllTodos();
  }

}

function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}
