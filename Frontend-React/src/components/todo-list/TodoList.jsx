import {  Button, Table } from 'react-bootstrap'

export const ToDoList = ({ todos, getItems, markComplete }) => {
  return (
    <>
      <h1>
        Showing {todos.length} Item(s)
        <Button variant="primary" className="pull-right" onClick={() => getItems()}>
          Refresh
        </Button>
      </h1>

      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {todos.map((todo, index) => (
            <tr key={todo.id}>
              <td>{index + 1}</td>
              <td>{todo.description}</td>
              <td>
                <Button data-testid={`toggle-complete-${todo.id}`} variant="warning" size="sm" onClick={() => markComplete(todo)}>
                  {todo.isCompleted? "Mark as incompleted" : "Mark as completed"}
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}