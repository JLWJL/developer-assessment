import './App.css'
import { Image, Alert, Container, Row, Col } from 'react-bootstrap'
import React, { useState, useEffect } from 'react'
import api from './http/todo-api'
import { AddTodo } from './components/add-todo/AddTodo'
import { ToDoList } from './components/todo-list/TodoList'

const App = () => {
  const [items, setItems] = useState([])

  const getTodos = async () => {
    try {
      const todos = await api.getAll();
      setItems(todos);
    }
    catch (err) {
      alert(err)
    }
  }

  const addTodo = async (newTodo) => {
    try {
      const addedTodo = await api.post(newTodo);
      setItems([...items, addedTodo]);
    }
    catch (err) {
      alert(err)
    }
  }

  const toggleComplete = async (item) => {
    try {
      const updated = await api.put(item.id, { ...item, isCompleted: !item.isCompleted });
      const todos = items.map(item => item.id === updated.id ? { ...updated } : item);
      setItems(todos);
    }
    catch (err) {
      alert(err)
    }
  }

  useEffect(() => {
    getTodos();
  }, [])


  return (
    <div className="App">
      <Container>
        <Row>
          <Col>
            <Image src="clearPointLogo.png" fluid rounded />
          </Col>
        </Row>
        <Row>
          <Col>
            <Alert variant="success">
              <Alert.Heading>Todo List App</Alert.Heading>
              Welcome to the ClearPoint frontend technical test. We like to keep things simple, yet clean so your
              task(s) are as follows:
              <br />
              <br />
              <ol className="list-left">
                <li>Add the ability to add (POST) a Todo Item by calling the backend API</li>
                <li>
                  Display (GET) all the current Todo Items in the below grid and display them in any order you wish
                </li>
                <li>
                  Bonus points for completing the 'Mark as completed' button code for allowing users to update and mark
                  a specific Todo Item as completed and for displaying any relevant validation errors/ messages from the
                  API in the UI
                </li>
                <li>Feel free to add unit tests and refactor the component(s) as best you see fit</li>
              </ol>
            </Alert>
          </Col>
        </Row>
        <Row>
          <Col><AddTodo addTodo={addTodo} /></Col>
        </Row>
        <br />
        <Row>
          <Col><ToDoList todos={items} getItems={getTodos} markComplete={toggleComplete} /></Col>
        </Row>
      </Container>
      <footer className="page-footer font-small teal pt-4">
        <div className="footer-copyright text-center py-3">
          © 2021 Copyright:
          <a href="https://clearpoint.digital" target="_blank" rel="noreferrer">
            clearpoint.digital
          </a>
        </div>
      </footer>
    </div>
  )
}

export default App
