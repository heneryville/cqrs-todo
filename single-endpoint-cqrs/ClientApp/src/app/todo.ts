export class Todo {
  id: string;
  title: string = '';
  isCompleted: boolean = false;

  constructor(values: Object = {}) {
    Object.assign(this, values);
  }
}
