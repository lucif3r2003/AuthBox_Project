import axios from 'axios';

export const login = async (data) => {
  return axios.post('http://localhost:3000/api/auth/login', data, {
    withCredentials: true,
  });
};

export const register = async (data) => {
  return axios.post('http://localhost:3000/api/auth/register', data, {
    withCredentials: true,
  });
};
