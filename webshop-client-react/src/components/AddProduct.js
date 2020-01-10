import React from 'react';

class AddProduct extends React.Component {
    constructor(props) { 
        super(props);
    }   
    render() {
        return ( 
            <button className="productButton" onClick={this.props.handleClick}>Add to cart</button>
        ); 
    }
   
} 

export default AddProduct;   