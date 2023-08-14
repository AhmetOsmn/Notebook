import { useEffect, useState } from 'react';
import Button from '../components/Button'
import { PostService, UserService } from './lesson12/services';

function Lesson12() {

    const [name, setName] = useState('ahmet');
    const [avatar, setAvatar] = useState(false);
    const [users, setUsers] = useState(false);

    // const addPost = (data) => {

    //     const headers = new Headers();
    //     // headers.append('Content-type', 'application/json')
    //     headers.append('Authorization', 'Bearer ahmet123abc')

    //     const formData = new FormData();
    //     formData.append('userId', data.userId);
    //     formData.append('title', data.title);
    //     formData.append('body', data.body);

    //     fetch('https://jsonplaceholder.typicode.com/posts', {
    //         method: 'POST',
    //         // body: JSON.stringify(data),
    //         body: formData,
    //         headers
    //     })
    //         .then(res => res.json())
    //         .then(data => console.log(data))
    //         .catch(error => console.log(error))
    // }

    useEffect(() => {
        // fetch('https://jsonplaceholder.typicode.com/users')
        //     .then(res => {
        //         if (res.ok && res.status === 200) {
        //             return res.json();
        //         }
        //     })
        //     .then(data => setUsers(data))
        //     .catch(error => console.log(error));

        UserService.getUsers().then(res => setUsers(res));

        // addPost({
        //     userId: 1,
        //     title: 'Örnek post',
        //     body: 'Post içeriği'
        // })

        PostService.getPosts().then(res => console.log(res))
        PostService.getPostDetail(1).then(res => console.log(res))
        PostService.addPost({
            userId: 1,
            title: 'Örnek post',
            body: 'Post içeriği'
        }).then(res => console.log(res))
    }, [])

    const submitHandler = e => {
        e.preventDefault();
        console.log("submit edildi");
    }

    return (
        <>
            <form onSubmit={submitHandler}>
                <input type='text' name='name' value={name} onChange={e => setName(e.target.value)} /><br />
                <input type='file' name='avatar' onChange={e => setAvatar(e.target.files[0])} /><br />
                <Button text="kaydet" disabled={!name || !avatar} type="submit" />
            </form>
            <h1>User List</h1>
            <ul>
                {users && users.map(user => (
                    <li key={user.id}>
                        {user.name}
                    </li>
                ))}
            </ul>
        </>
    );
}

export default Lesson12;