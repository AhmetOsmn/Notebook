import React, { useState, useEffect } from "react";
import axios from "axios";

function CountryList(props) {
    const [countries, setCountries] = useState([]);

    useEffect(() => {
        axios.get("https://restcountries.com/v3.1/lang/eng")
            .then(response => setCountries(response.data))
            .catch(error => console.log({ error }))
    }, []);

    return countries.filter(c => c.name.common.toLowerCase().includes(props.search.toLowerCase())).map(c => {
        return (
            <div key={c.name.common} className="country">
                <div className="imginfo">
                    <img src={c.flags.png} alt={c.name.common} />
                </div>
                <div className="cinfo">
                    <h3>{c.name.common}</h3>
                    <p>{c.capital}</p>
                </div>
            </div>
        );
    });
}

export default CountryList;