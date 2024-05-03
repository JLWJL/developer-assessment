import axios from 'axios';

const http = axios.create({ baseURL: process.env.REACT_APP_TODO_API_BASE_URL });
const path = '/todoItems';

const get = async (id) => {
  const response = await http.get(`${path}/${id}`);
  return response.data;
};

const getAll = async () => {
  const response = await http.get(path);
  return response.data;
};

const post = async (data) => {
  const response = await http.post(path, data);
  return response.data;
};

const put = async (id, data) => {
  const response = await http.put(`${path}/${id}`, data);
  return response.data;
};

export default {
  get,
  getAll,
  post,
  put,
};
