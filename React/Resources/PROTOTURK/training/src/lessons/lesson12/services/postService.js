import {post, get} from './request';

export const getPosts = () => get('posts');
export const getPostDetail = (id) => get(`posts/${id}`);
export const addPost = (data) => post('posts', data);
