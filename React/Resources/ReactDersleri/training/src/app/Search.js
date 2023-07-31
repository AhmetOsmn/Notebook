import React from 'react';

const SearchForm = (props) => {

    return (
        <form className='search'>
            <input
                name='search'
                placeholder='search country'
                value={props.search}
                onChange={props.onSearchChange} 
            />
        </form>
    );
}

export default SearchForm;
