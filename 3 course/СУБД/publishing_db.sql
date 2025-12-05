-- PostgreSQL database script for "Издательство" (Publishing house)
-- File: publishing_db.sql
-- Created: 2025-12-04

-- Drop existing tables (in order to recreate)
DROP TABLE IF EXISTS payments CASCADE;
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS editions CASCADE;
DROP TABLE IF EXISTS book_authors CASCADE;
DROP TABLE IF EXISTS books CASCADE;
DROP TABLE IF EXISTS authors CASCADE;
DROP TABLE IF EXISTS customers CASCADE;

-- 1. Authors
CREATE TABLE authors (
    author_id SERIAL PRIMARY KEY,
    full_name VARCHAR(150) NOT NULL,
    birth_year INTEGER,
    country VARCHAR(100)
);

-- 2. Books (conceptual work, one row per work / title)
CREATE TABLE books (
    book_id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    genre VARCHAR(100),
    isbn VARCHAR(20) UNIQUE
);

-- 3. Many-to-many: book_authors
CREATE TABLE book_authors (
    book_id INTEGER NOT NULL REFERENCES books(book_id) ON DELETE CASCADE,
    author_id INTEGER NOT NULL REFERENCES authors(author_id) ON DELETE CASCADE,
    contribution VARCHAR(100),
    PRIMARY KEY (book_id, author_id)
);

-- 4. Editions (physical/written editions published by the publishing house)
CREATE TABLE editions (
    edition_id SERIAL PRIMARY KEY,
    book_id INTEGER NOT NULL REFERENCES books(book_id) ON DELETE CASCADE,
    publisher VARCHAR(150) NOT NULL,
    pub_year INTEGER,
    format VARCHAR(50), -- e.g., Hardcover, Paperback, eBook
    price NUMERIC(10,2) NOT NULL
);

-- 5. Customers
CREATE TABLE customers (
    customer_id SERIAL PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    phone VARCHAR(30),
    email VARCHAR(150)
);

-- 6. Orders
CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    customer_id INTEGER NOT NULL REFERENCES customers(customer_id) ON DELETE SET NULL,
    edition_id INTEGER NOT NULL REFERENCES editions(edition_id) ON DELETE RESTRICT,
    order_date DATE DEFAULT CURRENT_DATE,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    total_price NUMERIC(12,2) NOT NULL,
    status VARCHAR(30) DEFAULT 'Pending'
);

-- 7. Payments
CREATE TABLE payments (
    payment_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NULL REFERENCES orders(order_id) ON DELETE CASCADE,
    amount NUMERIC(12,2) NOT NULL,
    payment_date DATE DEFAULT CURRENT_DATE,
    method VARCHAR(50)
);

-- Sample data inserts
INSERT INTO authors (full_name, birth_year, country) VALUES
('Александр Петров', 1978, 'Россия'),
('Мария Иванова', 1985, 'Россия'),
('John Smith', 1969, 'UK');

INSERT INTO books (title, genre, isbn) VALUES
('Мастер и Маргарита. Современное издание', 'Роман', '978-5-00000-000-1'),
('Python для издателей', 'Техническая литература', '978-5-00000-000-2'),
('Short Stories Collection', 'Stories', '978-1-23456-789-7');

INSERT INTO book_authors (book_id, author_id, contribution) VALUES
(1, 1, 'Автор'),
(2, 2, 'Автор'),
(3, 3, 'Составитель');

INSERT INTO editions (book_id, publisher, pub_year, format, price) VALUES
(1, 'Издательство "Луч"', 2024, 'Hardcover', 1200.00),
(1, 'Издательство "Луч"', 2025, 'Paperback', 800.00),
(2, 'TechBooks', 2023, 'Paperback', 950.00),
(3, 'GlobalPub', 2020, 'eBook', 300.00);

INSERT INTO customers (name, phone, email) VALUES
('ООО "Книжный Маг"', '+7-900-000-00-01', 'store@example.ru'),
('Иванов И.И.', '+7-900-000-00-02', 'ivanov@example.ru');

INSERT INTO orders (customer_id, edition_id, order_date, quantity, total_price, status) VALUES
(1, 1, '2025-11-30', 10, 12000.00, 'Completed'),
(2, 2, '2025-11-25', 1, 800.00, 'Pending');

INSERT INTO payments (order_id, amount, payment_date, method) VALUES
(1, 12000.00, '2025-11-30', 'Bank transfer');

-- Indexes for faster queries
CREATE INDEX idx_books_genre ON books(genre);
CREATE INDEX idx_editions_pubyear ON editions(pub_year);

-- Example view: current stock value per edition (not tracking inventory in schema here)
-- CREATE VIEW edition_prices AS SELECT e.edition_id, b.title, e.publisher, e.pub_year, e.format, e.price FROM editions e JOIN books b USING (book_id);

-- End of script