import React from 'react';

export default function Presentation({ author, text, title }) {
    return (
        <div>
        <h3>{title}</h3>
        <p>{text}</p>
        <p><em>by {author}</em></p>
        </div>
    );
}