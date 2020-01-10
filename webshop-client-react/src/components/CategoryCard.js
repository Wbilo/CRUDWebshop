import React from 'react'; 
function CategoryCard(props) {

    return ( 
            <button onClick={() => props.onClick(props.name)} className="CategoryLink">{props.name}</button>
    )      
}   
export default CategoryCard; 
 