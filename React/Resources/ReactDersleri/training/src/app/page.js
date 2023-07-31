
"use client";

import React, { useEffect, useState } from "react";
import CountryList from "./CountryList";
import SearchForm from "./Search";

const data = [
  { id: 1, name: "Ahmet " },
  { id: 2, name: "Ali " },
  { id: 3, name: "Veli " },
  { id: 4, name: "Mehmet " },
  { id: 5, name: "Osman " }
]

export default function Home() {
  //return StateExample();
  //return ConditionalRenderingExample();
  //return OnChangeExample();  
  //return FormExample();  
  //return LiftingStateUp();

  return (<div></div>);

}

function LiftingStateUp()
{
  const [search, setSearch] = useState("");

  const handleChange = event => setSearch(event.target.value);

  return (
    <div>
      <h1>Ülkeler</h1>
      <SearchForm search={search} onSearchChange={handleChange} />
      <CountryList search={search} onSearchChange={handleChange} />
    </div>
  );
}

function FormExample() {
  const [form, setForm] = useState({ name: "", city: "", bio: "" });

  console.log({ form });

  const handleChange = event => setForm({ ...form, [event.target.name]: event.target.value })

  return (
    <div>
      <h1>Hoşgeldiniz</h1>
      <form className="myForm">
        <input value={form.name} onChange={handleChange} name="name" type="text" placeholder="isminiz" />
        <select value={form.city} onChange={handleChange} name="city">
          <option value="" disabled> Şehir seçiniz</option>
          <option value="ankara">Ankara</option>
          <option value="istanbul">İstanbul</option>
          <option value="izmir">İzmir</option>
        </select>
        <textarea value={form.bio} onChange={handleChange} rows="10" name="bio" placeholder="biyografi" />
        <button>Gönder</button>
      </form>
    </div>
  );
}

function OnChangeExample() {
  const [users, setUsers] = useState(data);

  const handleChange = search => {
    let filteredData = data.filter(x => x.name.toLowerCase().includes(search.toLowerCase()))
    setUsers(filteredData);
  };

  return (
    <div>
      <h1>React</h1>
      <input onChange={(e) => handleChange(e.target.value)} placeholder="arama" />
      {users.map((user) => {
        return <User key={user.id} user={user} />
      })}
    </div>
  );
}

function User(props) {
  return <p style={{ border: "1px solid #CCC" }}>{props.user.name}</p>
}

function ConditionalRenderingExample() {
  const [name, setName] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const handleChange = event => setName(event.target.value);

  const handleSubmit = name => {
    if (name.length < 6) {
      setErrorMessage("En az 6 karakter giriniz.");
    }
    else {
      setErrorMessage("");
      setIsLoggedIn(true);
    }
  };

  return (
    <div>
      {isLoggedIn && <h1>Giriş Yapıldı!</h1>}
      {isLoggedIn ?
        <LoggedIn
          name={name}
          setIsLoggedIn={setIsLoggedIn}
          setName={setName} /> :
        <LoggedOut
          name={name}
          handleSubmit={handleSubmit}
          handleChange={handleChange}
          errorMessage={errorMessage} />}
    </div>
  );
}

function LoggedIn(props) {
  return <div>
    <h1>Hoşgeldiniz, {props.name}!</h1>
    <button onClick={() => { props.setIsLoggedIn(false); props.setName("") }}>Çıkış yap</button>
  </div>
}

function LoggedOut(props) {
  return <div>
    <input name="name" placeholder="İsim" value={props.name} onChange={props.handleChange} />
    <button onClick={() => props.handleSubmit(props.name)}>Giriş</button>
    <br />
    {props.errorMessage ? <h4>{props.errorMessage}</h4> : <h1>Giriş Yapınız</h1>}
  </div>
}

function StateExample() {
  const [name, setName] = useState("Ahmet");

  return (
    <div>
      <Hi name={name} />
      <button onClick={() => setName("Osman")}>İsim değiştir</button>
      <Message />
    </div>
  );
}

function Hi(props) {
  return <h1>Merhaba, {props.name}.</h1>
}

function Message(props) {
  const [isFirst, setIsFirst] = useState(true);

  useEffect(() => {
    console.log("Message useEffect");
  }, [isFirst]);

  return (
    <div>
      {isFirst ? (<h1>Mesaj 1</h1>) : (<h1>Mesaj 2</h1>)}
      <button onClick={() => setIsFirst(!isFirst)}>Mesaj değiştir</button>
    </div>
  );
}
